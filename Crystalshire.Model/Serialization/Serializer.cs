using Crystalshire.Model.Models;

using System.Drawing.Imaging;

namespace Crystalshire.Model.Serialization;

public abstract class Serializer {

    public void Save(Class model, string file) {
        int count;

        using var s = new FileStream(file, FileMode.Create, FileAccess.Write);
        using var w = new BinaryWriter(s);

        w.Write(model.Id);
        w.Write(model.Name);

        Write(w, model.Attack);
        Write(w, model.Death);
        Write(w, model.Ressurrection);
        Write(w, model.Talk);
        Write(w, model.Gathering);
        Write(w, model.Walking);
        Write(w, model.Running);
        Write(w, model.Idle);

        count = model.Specials.Count;

        w.Write(count);

        for (var i = 0; i < count; ++i) {
            Write(w, model.Specials[i]);
        }

        count = model.Emotes.Count;

        w.Write(count);

        for (var i = 0; i < count; ++i) {
            Write(w, model.Emotes[i]);
        }
    }

    public Class? Load(string file) {
        if (File.Exists(file)) {
            int count;

            using var s = new FileStream(file, FileMode.Open, FileAccess.Read);
            using var r = new BinaryReader(s);

            var model = new Class {
                Id = r.ReadInt32(),
                Name = r.ReadString(),
                Attack = ReadDirections(r),
                Death = ReadDirections(r),
                Ressurrection = ReadDirections(r),
                Talk = ReadDirections(r),
                Gathering = ReadDirections(r),
                Walking = ReadDirections(r),
                Running = ReadDirections(r),
                Idle = ReadDirections(r)
            };

            count = r.ReadInt32();

            for (var i = 0; i < count; ++i) {
                model.Specials.Add(ReadDirections(r));
            }

            count = r.ReadInt32();

            for (var i = 0; i < count; ++i) {
                model.Emotes.Add(ReadDirections(r));
            }

            return model;
        }

        return null;
    }

    private void Write(BinaryWriter w, Directions directions) {
        w.Write(directions.Id);
        w.Write(directions.Name);

        Write(w, directions.Up);
        Write(w, directions.Down);
        Write(w, directions.Left);
        Write(w, directions.Right);
    }

    private Directions ReadDirections(BinaryReader r) {
        var directions = new Directions() {
            Id = r.ReadInt32(),
            Name = r.ReadString()
        };

        directions.Up = ReadMovement(r);
        directions.Down = ReadMovement(r);
        directions.Left = ReadMovement(r);
        directions.Right = ReadMovement(r);

        return directions;
    }

    private void Write(BinaryWriter w, Movement movement) {
        var count = movement.Count;

        w.Write(movement.Count);

        w.Write(movement.Name);
        w.Write(movement.Time);
        w.Write(movement.Continuously);
        w.Write(movement.WaitResponse);

        for (var i = 0; i < count; ++i) {
            var frame = movement[i];

            if (frame is not null) {
                Write(w, frame);
            }
        }
    }

    private Movement ReadMovement(BinaryReader r) {
        var count = r.ReadInt32();

        var movement = new Movement() {
            Name = r.ReadString(),
            Time = r.ReadInt32(),
            Continuously = r.ReadBoolean(),
            WaitResponse = r.ReadBoolean()
        };

        for (var i = 0; i < count; ++i) {
            movement.Add(ReadFrame(r));
        }

        return movement;
    }

    private void Write(BinaryWriter w, Frame frame) {
        byte[]? buffer = null;
        var length = 0;

        if (frame.Image is not null) {
            buffer = GetBytes(frame.Image);
            length = buffer.Length;
        }

        w.Write(frame.Name);
        w.Write(length);

        if (buffer is not null) {
            w.Write(buffer);
        }

        w.Write(frame.Width);
        w.Write(frame.Height);
        w.Write(frame.CanMove);
        w.Write((int)frame.AttackType);
        w.Write(frame.CastSkillId);
        w.Write(frame.Animation.Id);
        w.Write(frame.Animation.OffsetX);
        w.Write(frame.Animation.OffsetY);
    }

    private Frame ReadFrame(BinaryReader r) {
        int length;

        var frame = new Frame {
            Name = r.ReadString()
        };

        length = r.ReadInt32();

        if (length > 0) {
            frame.Image = GetBitmap(r.ReadBytes(length));
        }

        frame.Width = r.ReadInt32();
        frame.Height = r.ReadInt32();
        frame.CanMove = r.ReadBoolean();
        frame.AttackType = (AttackType)r.ReadInt32();
        frame.CastSkillId = r.ReadInt32();

        frame.Animation = new Animation() {
            Id = r.ReadInt32(),
            OffsetX = r.ReadInt32(),
            OffsetY = r.ReadInt32()
        };

        return frame;
    }

    protected virtual byte[] GetBytes(Bitmap image) {
        var ms = new MemoryStream();
        image.Save(ms, ImageFormat.Png);

        return ms.ToArray();
    }

    protected virtual Bitmap GetBitmap(byte[] buffer) {
        var ms = new MemoryStream(buffer);

        return new Bitmap(ms);
    }
}